'use client'
import React, {useState, useEffect} from 'react'
import Script from "next/script";
import Image from "next/image";
import Games from "@/components/games";
import Pagination from "@/components/pagination";

const GetGames = async (s:string,p?:number) =>{
    const res = await fetch(process.env.API_URL+
        "/api/Game?Studio="+s+"&Page="+(p??1)+"&PageSize=12");
    return res.json();
}

export default function StudioGames(studios) {
    const [gamesList, setGamesList] = useState(0);
    const [pagination, setPagination] = useState(0);

    const fetchGames = async (s: string) => {
        setGamesList("");
        const games = await GetGames(s);
        setGamesList(games);
        setPagination({pages:games,studio:s});
    }
    useEffect(() => {gamesList});
    useEffect(() => {pagination});

    return (
        <>
            <section className="game-list">
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
                                onClick={(e) => fetchGames("")}>All</button>
                        {studios.studios.map((studio) => (
                            <button key={studio.Title} className="carousel-item"
                                    onClick={(e) => fetchGames(studio.Title)}>{studio.Title}</button>
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

                <Games games={gamesList}/>
                <div className="pagination">
                    <Pagination pages={pagination}/>
                </div>
            </section>
            <Script src="/functions.js" onLoad={() => {
                intractTabs();
                fetchGames("");
            }}/>
        </>
    )
}