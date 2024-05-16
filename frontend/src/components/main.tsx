'use client'
import React, {useState, useEffect} from 'react'
import Script from "next/script";
import Games from "@/components/games";
import Pagination from "@/components/pagination";
import StudioTabs from "@/components/tabs";

const GetGames = async (s:string,p?:number) =>{
    const res = await fetch(process.env.API_URL+
        "/api/Game?Studio="+s+"&Page="+(p??1)+"&PageSize=12");
    return res.json();
}

const GetStudios = async () => {
    const res = await fetch(process.env.API_URL+'/api/Game/studios');
    return res.json();
}

export default function StudioGames() {
    const [gamesList, setGamesList] = useState(null);
    const [pagination, setPagination] =
        useState({pages:null,studio:""});
    const [pageNumber, setPageNumber] = useState(1);
    const [studioName, setStudioName] = useState("");
    const [studios, setStudios] = useState(null);

    const fetchGames = async (s: string,page?:number) => {
        setGamesList(null);// empty game list for css animation
        const games = await GetGames(s,page);
        setGamesList(games);
        setPagination({pages:games,studio:s});
    }
    useEffect(()=>{
        fetchGames(studioName, pageNumber).then(r => {});
    },[pageNumber,studioName]);

    return (
        <>
            <section className="game-list">
                <StudioTabs setStudioName={setStudioName} studios={studios} setPageNumber={setPageNumber}/>

                <Games games={gamesList}/>
                <div className="pagination">
                    <Pagination setPageNumber={setPageNumber} pages={pagination.pages} studio={pagination.studio}/>
                </div>
            </section>
            <Script src="/functions.js" onLoad={() => {
                GetStudios().then(v =>{
                    setStudios(v);
                    intractTabs();
                    fetchGames("");
                });

            }}/>
        </>
    )
}