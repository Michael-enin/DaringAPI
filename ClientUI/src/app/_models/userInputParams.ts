import { User } from "./user";

export class UserInputParams{
    gender:string;
    minAge=28;
    maxAge=120;
    pageNumber=1;
    pageSize=5;
    orderBy='lastActive'
    constructor(user:User){
        this.gender=user.gender==="female" ? "male":"female";
    }
}