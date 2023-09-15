import { ForumData } from "./forum-data";
import { PostData } from "./post-data";

export interface ForumPageData {
    forum: ForumData,
    posts: PostData[]
}