## Build SVG sprite sheet (manual)
A nice and efficient solution for reusing and preloading SVGs is with &lt;use>

`npm install svg-sprite -g`

`cd wwwroot/`

`npx svg-sprite --symbol --dest=./ raw-svg/*.svg --log=verbose`

NOTE: use verbose to check for errors. Such as BOM encoded svg files. Using Notepad++ to strip the BOM is a solution
