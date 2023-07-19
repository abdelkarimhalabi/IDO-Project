export class TaskCard{
    id : number = 0;
    userId : number = 0;
    statusId : number = 1;
    importanceId : number = 1;
    title: string = 'Card Title';
    category: string = 'Category Value';
    estimate: number = 5;
    importance: string = 'Low';
    position : number = -1;
    date : string = "2024/1/1";

    changeImportance(){
        if(this.importanceId == 1){
            this.importance = "Low"
        }
        else if(this.importanceId == 2){
            this.importance = "Medium"
        }
        else if(this.importanceId == 3){
            this.importance = "High"
        }
    }
}