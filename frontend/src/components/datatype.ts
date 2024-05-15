
type setPageNumber = (pageNumber: number) => void;
type setStudioName = (studio: string) => void;

export interface StudioList {
    studios: Studio[]
}
export interface StudioTabsData {
    studios: Studio[],
    setStudioName:setStudioName,
    setPageNumber:setPageNumber,
}
export interface Studio {
    Title: string,
    Games: number,
}

export interface GameData {
    games: {
        Data: Game[],
    } | null
}

export interface Game {
    Id:string,
    GameTitle:string,
    Genre:string,
    "Platform":string,
    "ReleaseDate":Date,
    "Rating":number,
    Price:number,
    ImageUrl:string,
    Description:string,
    StudioName:string,
}

export interface PageData {
    setPageNumber: setPageNumber,
    pages: Page | null,
    studio: string,
    data?: { studio: string; pages: null }
}

interface Page {
    TotalPages:number,
    Page:number,
    TotalItems:number,
    Sort:string,
    PageSize:number
}