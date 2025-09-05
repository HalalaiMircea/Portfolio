export function makeWindowDraggable(windowEl) {
    let pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;

    windowEl.querySelector(".title-bar").onmousedown = (e) => {
        if (e.button !== 0) return;
        if (e.target.closest(".title-bar-controls, .title-bar-extra, button, input")) return;
        e.preventDefault();

        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = () => {
            document.onmouseup = null;
            document.onmousemove = null;
        };
        document.onmousemove = elementDrag;
    };

    function elementDrag(e) {
        e.preventDefault();
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        windowEl.style.top = (windowEl.offsetTop - pos2) + "px";
        windowEl.style.left = (windowEl.offsetLeft - pos1) + "px";
    }

}

export function getRectForEl(el) {
    const rect = el.getBoundingClientRect();
    return {
        top: rect.top,
        left: rect.left,
        width: rect.width,
        height: rect.height
    };
}

/*
export function maximizeWindow(windowEl) {
    windowEl.style.top = "0px";
    windowEl.style.left = "0px";
    
    windowEl.style.width = "100vw";
    windowEl.style.height = "100vh";
}
*/
