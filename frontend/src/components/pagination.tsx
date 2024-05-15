import React from "react";

export default function Pagination(data) {
    if (!data || data.pages ==0 || data.pages.pages.TotalPages == 1) return <></>;
    const studio = data.studio;
    const pagination = data.pages.pages
    const isFirst = pagination.Page == 1;
    const isLast = pagination.Page == pagination.TotalPages;
    let start = pagination.Page;
    if (!isFirst) {
        for (let i = pagination.Page; i >= pagination.Page - 2; i--) {
            start = i;
        }
    }
    const pages = [];
    for (let i = start; i < pagination.TotalPages; i++) {
        pages.push(i)
        if (pages.length == 5) {
            break;
        }
    }

     const ChangePage = (e:MouseEvent,page:number,studio:string) => {
        e.preventDefault();
        console.log(page,studio,e)
    }


    return (
        <>
                <a href="#" className={isFirst ? "inactive" : ""}>&laquo;</a>
                {
                    pages.map((p) =>
                        <a key={p} href="#"
                           className={pagination.Page==p?"active":""}
                           onClick={(e) =>ChangePage(e,p,studio)}>{p}</a>
                    )
                }
                <a className={isLast ? "inactive" : ""} onClick={(e) =>ChangePage(e,pagination.TotalPages,studio)}>&raquo;</a>
        </>
    )
}