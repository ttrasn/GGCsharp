import Image from "next/image";
import React from "react";


export default function Games(data) {
    return (
        <>
            <section className="games" id="games">
        {data.games?.Data?.map((g) =>
            <div key={g.Id} className="game">
                <a href="http://google.com/">
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