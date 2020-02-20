<p align="center">
<img alt="CodeGraph Banner" src="/img/readme_banner.jpg" width=920>  
</p>
<p align="center">
<a href="https://github.com/TeodorVecerdi/CodeGraph/issues"><img alt="GitHub issues" src="https://img.shields.io/github/issues-raw/TeodorVecerdi/CodeGraph?color=e62c0b&label=issues"></a> <a href="https://www.codacy.com/manual/TeodorVecerdi/CodeGraph?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=TeodorVecerdi/CodeGraph&amp;utm_campaign=Badge_Grade"><img alt="Codacy grade" src="https://img.shields.io/codacy/grade/4521530989444f0a8e00755a2faabb1e"></a><a href="https://codescene.io/projects/6802"><img alt="CodeScene Code Health" src="https://codescene.io/projects/6802/status-badges/code-health"></a><a href="https://github.com/TeodorVecerdi/CodeGraph/releases/latest"><img alt="GitHub release (latest by date including pre-releases)" src="https://img.shields.io/github/v/release/TeodorVecerdi/CodeGraph?include_prereleases&label=release"></a> <a href="https://github.com/TeodorVecerdi/CodeGraph/stargazers"><img alt="GitHub stars" src="https://img.shields.io/github/stars/TeodorVecerdi/CodeGraph?color=FFD700"></a> <a href="https://github.com/TeodorVecerdi/CodeGraph/graphs/contributors"><img alt="GitHub contributors" src="https://img.shields.io/github/contributors-anon/TeodorVecerdi/CodeGraph?color=009a00"></a> <a href="https://github.com/TeodorVecerdi/CodeGraph/blob/master/LICENSE"><img alt="GitHub License" src="https://img.shields.io/github/license/TeodorVecerdi/CodeGraph"></a>
</p>
<p align="justify">
  <b>Code Graph</b> is an open-source <b>visual programming</b> tool for artists, designers and other non-programmers alike who want to make games but don't know how to program.
  <br><sup><sub align="justify"><b>Code Graph is in a very early development stage. Most important features and probably missing at this point. If you want a working tool follow the repo and check back later</b></sub></sup>
</p>

<h1 align="center">CONTRIBUTORS</h1>

If you would like to contribute to this project feel free to submit a **pull request**. I need help mostly on the frontend side of the project, with the UI, nodes, files and importer, but help in other places such as the backend is always welcome. To see where help is needed check out the *Todo* Section below to get a broad overview of main features that need to be implemented.

<h1 align="center">TODO (Last edited 02/02/2020 at 12:47)</h1>
<h3>Frontend</h3>

~~- [ ] Implement a basic UI using the in-game UI elements~~
- [x] Implement UI using a custom EditorWindow (*Custom EditorWindow is done*)
  - [ ] Implement nodes and connections on the UI
<h3>Backend</h3>

- [x] Add a way to remove connection between nodes
- [x] Add a way to have multiple connections from one Output node to Input nodes
- [ ] Provide error checking in a more useful way (aka don't tell the user that a ; is missing on line #xxxx when the user doesn't even look at the code generated)
- [ ] Add properties / variables
- [ ] Add functions (with inputs and outputs defined, aka user can create own nodes in the graph and reuse them later)


<h1 align="center">Progress so far</h1>

- Basic fundamental nodes defined on the backend
- Able to generate code, compile said code and check for errors
- Custom EditorWindow is mostly implemented
- CodeGraph files are mostly implemented & have a custom importer (Some errors happen on reimport that don't affect the user at all)


**Example of generated code from nodes and error given**  
![Example of generated code from nodes and error](img/readme_img1.jpeg "Example of generated code from nodes and error")
