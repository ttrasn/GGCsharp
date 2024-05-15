import Image from "next/image";
import React from "react";
import {Game, GameData} from "@/components/datatype";


export default function Games({games}:GameData) {
    return (
        <>
            <section className="games" id="games">
        {games?.Data?.map((g:Game) =>
            <div key={g.Id} className="game">
                <a href={"https://gamesglobal.com/games/"+g.StudioName+"/"+g.GameTitle} target="_blank" rel="noopener noreferrer">
                    <Image
                        src={g.ImageUrl}
                        width={120}
                        height={120}
                        alt={g.GameTitle}/>
                </a>
            </div>
        )}
            </section>
        </>
    )
}