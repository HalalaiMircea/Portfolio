export function makeWindowDraggable(windowEl) {
    let distSinceLastX = 0, distSinceLastY = 0, prevX = 0, prevY = 0;

    windowEl.querySelector(".title-bar").onmousedown = (e) => {
        if (e.button !== 0) return;
        if (e.target.closest(".title-bar-controls, .title-bar-extra, button, input")) return;
        e.preventDefault();

        prevX = e.clientX;
        prevY = e.clientY;
        document.onmouseup = () => {
            document.onmouseup = null;
            document.onmousemove = null;
        };
        document.onmousemove = elementDrag;
    };

    function elementDrag(e) {
        e.preventDefault();
        if (e.clientX < 0 || e.clientX > window.innerWidth) return;
        if (e.clientY < 0 || e.clientY > window.innerHeight) return;

        distSinceLastX = prevX - e.clientX;
        distSinceLastY = prevY - e.clientY;
        windowEl.style.top = (windowEl.offsetTop - distSinceLastY) + "px";
        windowEl.style.left = (windowEl.offsetLeft - distSinceLastX) + "px";
        prevX = e.clientX;
        prevY = e.clientY;
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
