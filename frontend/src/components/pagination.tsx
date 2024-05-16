import React from "react";
import { MouseEvent } from 'react';
import {PageData} from "@/components/datatype";


export default function Pagination({setPageNumber, pages, studio}:PageData) {
    if (pages == null || pages.TotalPages == 1) return <></>;
    const pagination = pages
    const isFirst = pagination.Page == 1;
    const isLast = pagination.Page == pagination.TotalPages;
    let start = pagination.Page;
    if (!isFirst) {
        for (let i = pagination.Page; (i >= pagination.Page - 2) && i != 0; i--) {
            start = i;
        }
    }
    const pageNumbers = [];
    for (let i = start; i <= pagination.TotalPages; i++) {
        pageNumbers.push(i);
        if (pageNumbers.length == 5) {
            break;
        }
    }

     const ChangePage = (e:MouseEvent,page:number,studio:string) => {
        e.preventDefault();
        setPageNumber(page);
    }


    return (
        <>
                <a href="#" className={isFirst ? "inactive" : ""}
                   onClick={(e) =>ChangePage(e,1,studio)}>&laquo;</a>
                {
                    pageNumbers.map((p) =>
                        <a key={p} href="#"
                           className={pagination.Page==p?"active":""}
                           onClick={(e) =>ChangePage(e,p,studio)}>{p}</a>
                    )
                }
                <a className={isLast ? "inactive" : ""}
                   onClick={(e) =>ChangePage(e,pagination.TotalPages,studio)}>&raquo;</a>
        </>
    )
}