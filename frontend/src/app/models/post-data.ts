export interface PostData {
    id: number,
    title: string,
    content: string,
    createAt: Date,
    vote: boolean | null
}