<p align="center">
<img alt="CodeGraph Banner" src="/img/readme_banner.jpg" width=920>  
</p>
<p align="center">
<a href="https://github.com/TeodorVecerdi/CodeGraph/issues"><img alt="GitHub issues" src="https://img.shields.io/github/issues-raw/TeodorVecerdi/CodeGraph?color=e62c0b&label=issues&style=for-the-badge"></a> <a href="https://www.codacy.com/manual/TeodorVecerdi/CodeGraph?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=TeodorVecerdi/CodeGraph&amp;utm_campaign=Badge_Grade"><img alt="Codacy grade" src="https://img.shields.io/codacy/grade/4521530989444f0a8e00755a2faabb1e?style=for-the-badge&label=code"></a> <a href="https://github.com/TeodorVecerdi/CodeGraph/releases/latest"><img alt="GitHub release (latest by date including pre-releases)" src="https://img.shields.io/github/v/release/TeodorVecerdi/CodeGraph?include_prereleases&label=release&style=for-the-badge"></a> <a href="https://github.com/TeodorVecerdi/CodeGraph/stargazers"><img alt="GitHub stars" src="https://img.shields.io/github/stars/TeodorVecerdi/CodeGraph?color=FFD700&style=for-the-badge"></a> <a href="https://github.com/TeodorVecerdi/CodeGraph/graphs/contributors"><img alt="GitHub contributors" src="https://img.shields.io/github/contributors-anon/TeodorVecerdi/CodeGraph?color=009a00&style=for-the-badge"></a> <a href="https://github.com/TeodorVecerdi/CodeGraph/blob/master/LICENSE"><img alt="GitHub License" src="https://img.shields.io/github/license/TeodorVecerdi/CodeGraph?style=for-the-badge"></a>
</p>
<p align="justify">
  <b>Code Graph</b> is an open-source <b>visual programming</b> tool for artists, designers and other non-programmers alike who want to make games but don't know how to program. Code Graph is similar in functionality to the Shader Graph addon by Unity.
</p>

# TODO (Last edited 27/01/2020 at 15:12)
## Frontend
~~- [ ] Implement a basic UI using the in-game UI elements~~
- [ ] Implement UI using a custom EditorWindow
## Backend
- [x] Add a way to remove connection between nodes
- [x] Add a way to have multiple connections from one Output node to Input nodes
- [ ] Provide error checking in a more useful way (aka don't tell the user that a ; is missing on line #xxxx when the user doesn't even look at the code generated)

# Progress so far
- Basic fundamental nodes defined on the backend
- Able to generate code, compile said code and check for errors


**Example of generated code from nodes and error given**

![Example of generated code from nodes and error](img/readme_img1.jpeg "Example of generated code from nodes and error")
