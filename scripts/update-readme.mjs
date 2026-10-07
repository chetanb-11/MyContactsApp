import fs from "fs";
import { execSync } from "child_process";
import { GoogleGenAI } from "@google/genai";

async function main() {
  if (!process.env.GEMINI_API_KEY) {
    throw new Error("GEMINI_API_KEY is not defined in environment variables.");
  }

  // 1. Get git diff and commit log
  let diff = "";
  try {
    diff = execSync('git diff HEAD~1 HEAD --stat -p -- . ":(exclude)README.md" ":(exclude)package-lock.json"')
        .toString()
        .slice(0, 15000);
  } catch (err) {
    console.log("Could not extract diff, proceeding with general context.");
  }

  const commitMsg = execSync("git log -1 --pretty=%B").toString().trim();
  const currentReadme = fs.existsSync("README.md") ? fs.readFileSync("README.md", "utf8") : "";

  // 2. Fetch project file structure for context
  let projectTree = "";
  try {
    projectTree = execSync("git ls-files").toString().split("\n").filter(Boolean).slice(0, 80).join("\n");
  } catch (_) {}

  const ai = new GoogleGenAI({ apiKey: process.env.GEMINI_API_KEY });

  const prompt = `
You are a technical documentation specialist.
Your task is to generate/update the complete, production-grade GitHub README.md for this .NET repository.

REPOSITORY CONTEXT:
- Commit message: ${commitMsg}
- Key repository files:
${projectTree}

LATEST GIT CHANGES (DIFF):
${diff || "Initial README generation"}

CURRENT README:
${currentReadme || "(Empty file)"}

INSTRUCTIONS:
1. Provide a clean, accurate GitHub README.md in raw Markdown.
2. Include:
   - Project title and clear summary of what the project does.
   - Tech Stack (.NET, C#, Architecture layers like Core, Infrastructure, Web).
   - Project Structure / Architecture overview.
   - Setup & Run instructions (dotnet build, dotnet run).
   - Features or use cases added based on the commits/diff.
3. STRICT NEGATIVE CONSTRAINTS:
   - DO NOT include a "License" section or mention MIT / licensing.
   - DO NOT include a "Contact", "Author", or "Contributing" section.
   - DO NOT generate placeholder emails (e.g., your.email@example.com) or fake social links.
   - DO NOT output introductory text, notes, or meta commentary.
4. Output ONLY the raw Markdown content. Do not wrap the final output in outer \`\`\`markdown code fences.
`;

  const response = await ai.models.generateContent({
    model: "gemini-2.5-flash",
    contents: prompt,
  });

  let text = response.text ? response.text.trim() : "";

  // Strip wrapping markdown code blocks if the model wrapped its output
  if (text.startsWith("```markdown")) text = text.slice(11);
  if (text.startsWith("```")) text = text.slice(3);
  if (text.endsWith("```")) text = text.slice(0, -3);

  text = text.trim();

  // Programmatic fallback cleanup to strip License or Contact sections if generated
  text = text.replace(/##?\s*(License|Licence|Contact|Contributing)[\s\S]*?(?=(##?\s|$))/gi, "").trim();

  if (!text) {
    throw new Error("Received empty response from Gemini API.");
  }

  fs.writeFileSync("README.md", text + "\n");
  console.log("README.md successfully updated without License/Contact sections!");
}

main().catch((err) => {
  console.error("Error updating README:", err);
  process.exit(1);
});