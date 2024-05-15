import Image from "next/image";
import StudioGames from "../components/main";


export async function GetStudios():Promise<[]> {
  const res = await fetch(process.env.API_URL+'/api/Game/studios');
  return res.json();
}

export default async function Home() {
  const studios = await GetStudios();
  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-24">
      <div className="z-10 w-full max-w-5xl items-center justify-between font-mono text-sm lg:flex">
        
        <div className="fixed bottom-0 left-0 flex h-48 w-full items-end justify-center bg-gradient-to-t from-white via-white dark:from-black dark:via-black lg:static lg:size-auto lg:bg-none">
          <a
            className="pointer-events-none flex place-items-center gap-2 p-8 lg:pointer-events-auto lg:p-0"
            href="https://gamesglobal.recruitee.com/o/full-stack-developer-2-4"
            target="_blank"
            rel="noopener noreferrer"
          >
            <Image
              src="/logo.png"
              alt="Games Global"
              width={100}
              height={24}
              priority
            />
          </a>
        </div>
      </div>

    <StudioGames studios={studios}/>

      <div className="mb-32 grid text-center lg:mb-0 lg:w-full lg:max-w-5xl lg:grid-cols-4 lg:text-left">
        
      </div>
    </main>
  );
}
