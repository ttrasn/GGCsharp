function scrollEv(leftArrow, rightArrow, carousel) {
    if (carousel.scrollLeft <= 0) {
        leftArrow.classList.add("arrow-inactive");
    } else {
        leftArrow.classList.remove("arrow-inactive");
    }
    if (carousel.scrollLeft >= carousel.scrollWidth - carousel.offsetWidth - 1) {
        rightArrow.classList.add("arrow-inactive");
    } else {
        rightArrow.classList.remove("arrow-inactive");
    }
}

function clickLeftArrow(carousel, rectList) {
    let shiftScroll;
    for (let i = 0; i < rectList.length; i++) {
        if (carousel.scrollLeft > rectList[rectList.length - 1]) {
            shiftScroll = rectList[rectList.length - 1];
        } else if (
            carousel.scrollLeft > rectList[i] &&
            carousel.scrollLeft <= rectList[i + 1]
        ) {
            shiftScroll = rectList[i];
        }
    }
    carousel.scrollTo({
        left: shiftScroll,
        behavior: "smooth"
    });
}

function clickRight(carousel, rectList) {
    let shiftScroll;
    for (let i = 0; i < rectList.length; i++) {
        if (
            carousel.scrollLeft >= rectList[i] - 1 &&
            carousel.scrollLeft < rectList[i + 1]
        ) {
            shiftScroll = rectList[i + 1];
        }
    }
    carousel.scrollTo({
        left: shiftScroll,
        behavior: "smooth"
    });
}

function listRectCarousel(carouselNumber, carousels) {
    let divs = carousels[carouselNumber].getElementsByClassName("carousel-item");
    let rectList = [];
    let rectGauche = carousels[carouselNumber].getBoundingClientRect().left;

    for (let i = 0; i < divs.length; i++) {
        let rect = divs[i].getBoundingClientRect();
        rectList.push(rect.left - rectGauche);
    }

    for (let i = rectList.length - 1; i >= 0; i--) {
        rectList[i] = rectList[i] - rectList[0];
    }
    return rectList;
}

function autoSlidePosLeft(carouselNumber, carousels, leftArrow) {
    let rectList = listRectCarousel(carouselNumber, carousels);
    leftArrow.addEventListener("click", () => {
        clickLeftArrow(carousels[carouselNumber], rectList);
    });
}

function autoSlidePosRight(carouselNumber, carousels, rightArrow) {
    let rectList = listRectCarousel(carouselNumber, carousels);
    rightArrow.addEventListener("click", () => {
        clickRight(carousels[carouselNumber], rectList);
    });
}

function intractTabs() {
    let leftArrow = document.getElementsByClassName("left-arrow")[0];
    let rightArrow = document.getElementsByClassName("right-arrow")[0];
    let carousels = document.getElementsByClassName("carousel");


    autoSlidePosRight(0, carousels, rightArrow);
    autoSlidePosLeft(0, carousels, leftArrow);
    window.onresize = () => {
        autoSlidePosRight(0, carousels, rightArrow);
        autoSlidePosLeft(0, carousels, leftArrow);
    };

    for (let i = 0; i < carousels.length; i++) {
        carousels[i].addEventListener("scroll", () => {
            scrollEv(leftArrow, rightArrow, carousels[i]);
        });
    }

    for (let i = 0; i < carousels.length; i++) {
        scrollEv(leftArrow, rightArrow, carousels[i]);
        window.onresize = () => {
            scrollEv(leftArrow, rightArrow, carousels[i]);
        };
    }

}
