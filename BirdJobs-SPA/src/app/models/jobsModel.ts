import { UserDetails } from './userDetails';


export interface JobsModel {
    results: Results[];
    next: string;
    
}

export interface Results {
    created_at: string;
    text: string;
    source: string;
    user: UserDetails;
    extended_tweet: ExtendedTweet;
    entities: Entities;
}

export interface ExtendedTweet {
    full_text: string;
}

export interface Entities { 
    urls: Urls[]
}

export interface Urls {
    url: string;
    expanded_url: string;
    display_url: string;
}