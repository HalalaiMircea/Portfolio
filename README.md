## Build SVG sprite sheet (manual)
A nice and efficient solution for reusing and preloading SVGs is with &lt;use>

`npm install svg-sprite -g`

`cd wwwroot/`

`npx svg-sprite --symbol --dest=./ raw-svg/*.svg --log=verbose --shape-id-generator i-%s`

NOTE: use verbose to check for errors. Such as BOM encoded svg files. Using Notepad++ to strip the BOM is a solution

# License
Copyright © 2025 Halalai Mircea

This source code is made available for viewing and reference purposes only.  
All rights are reserved by the copyright holder. 

You are not permitted to use, copy, modify, merge, publish, distribute, sublicense, or sell copies of this software or its contents, in whole or in part, without explicit written permission from the copyright holder.

For inquiries regarding licensing or usage, please contact me.
