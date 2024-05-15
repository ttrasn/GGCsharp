import {Studio, StudioTabsData} from "@/components/datatype";
import Image from "next/image";
import React from "react";


export default function StudioTabs({studios,setStudioName,setPageNumber}:StudioTabsData) {
    const updatePage = (s:string) =>{
        setStudioName(s);
        setPageNumber(1);
    }
    return (
        <>
            <section className="carousel-wrapper scrollbar-overflow">
                <button type="button" className="arrows left-arrow arrow-inactive" aria-label="Arrow Left">
                    <Image
                        priority
                        src="/left-btn.svg"
                        height={12}
                        width={12}
                        alt={"left button"}
                    />
                </button>
                <section className="carousel">
                    <button key="all" className="carousel-item"
                            onClick={(e) => updatePage("")}>All
                    </button>
                    {studios.map((studio: Studio) => (
                        <button key={studio.Title} className="carousel-item"
                                onClick={(e) => updatePage(studio.Title)}>{studio.Title}</button>
                    ))}
                </section>
                <button type="button" className="arrows right-arrow" aria-label="Arrow Right">
                    <Image
                        priority
                        src="/right-btn.svg"
                        height={12}
                        width={12}
                        alt={"right button"}
                    />
                </button>
            </section>
        </>
    );
}