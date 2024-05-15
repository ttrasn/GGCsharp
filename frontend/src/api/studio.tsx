export default async function GetGames(s:string) {
    const res = await fetch(process.env.API_URL+'/api/Game');
    console.log(res.json())

}