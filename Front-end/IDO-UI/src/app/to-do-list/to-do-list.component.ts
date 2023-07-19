import { Component, ElementRef, OnInit , HostListener , ViewChildren, QueryList, ViewChild} from '@angular/core';
import { TaskCard } from 'src/Models/TaskCard';
import { TokenService } from '../token.service';
import { TaskService } from '../task.service';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
  CdkDrag,
  CdkDropList,
} from '@angular/cdk/drag-drop';
import { TaskCardComponent } from '../task-card/task-card.component';
import { NavbarComponent } from '../navbar/navbar.component';



@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css']
})
export class ToDoListComponent implements OnInit {

  constructor(private tokenService : TokenService , private service : TaskService , private elementRef: ElementRef) { }

  todo : TaskCard[] = [];
  doing : TaskCard[] = [];
  done : TaskCard[] = [];

  isEditing: boolean = false;
  isSearchEmpty : boolean = false;
  searchText: string = '';
  
  @ViewChild(NavbarComponent) navbarComponent!: NavbarComponent;
  @ViewChildren(TaskCardComponent) cardComponents!: QueryList<TaskCardComponent>;
  receivedData?: TaskCard;

  ngOnInit(): void {
    this.service.getUserTasks(this.tokenService.getToken()).subscribe((tasksList : any) => {
      tasksList.forEach((task : any) => {
        let newTask = new TaskCard();
        Object.assign(newTask, task);
        console.log(newTask);
        newTask.date = this.convertDateFormat(newTask.date);
        newTask.changeImportance();
        if(newTask.statusId == 1){
          this.todo.push(newTask)
        }
        else if(newTask.statusId == 2){
          this.doing.push(newTask);
        }
        else if(newTask.statusId == 3){
          this.done.push(newTask);
        }
      })

      this.sortTasks();
    });
  }

  drop(event: CdkDragDrop<TaskCard[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }

    let droppedTaskCard: TaskCard = event.container.data[event.currentIndex];
    droppedTaskCard.position = event.currentIndex;
    droppedTaskCard = this.changeTaskContainer(droppedTaskCard , event.container.id);

    console.log(droppedTaskCard);

    this.service.editTask(droppedTaskCard , this.tokenService.getToken()).subscribe();

    const connectedToDropLists: CdkDropList<TaskCard[]>[] = event.container.connectedTo as CdkDropList<TaskCard[]>[];
    for (const dropList of connectedToDropLists) {
      // Access the data from each connected drop list
      const connectedData: TaskCard[] = dropList.data;
    }
  }

  convertDateFormat(inputDate: string): string {
    if (!inputDate) return '';
  
    const [day, month, year] = inputDate.split('/');
    const fullYear = '20' + year; 
  
    return `${fullYear}/${month}/${day}`;
   }

  toggleEditMode() {
    this.isEditing = !this.isEditing;
  }
  sortTasks(){
    this.todo.sort((a, b) => a.position - b.position);
    this.doing.sort((a, b) => a.position - b.position);
    this.done.sort((a, b) => a.position - b.position);
  }

  receiveData(data: TaskCard) {
    this.receivedData = data;
    if(data.position == -1){
      data.position = 0;
      this.service.createTask(data , this.tokenService.getToken()).subscribe();
    }
    else{
      this.service.editTask(data , this.tokenService.getToken()).subscribe();
    }
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    // Clicked outside the card, save data and toggle edit mode off
    this.cardComponents.forEach((cardComponent) => {
      if (cardComponent.isEditing) {
        cardComponent.saveDataAndToggleEditMode();
      }
    });
  }

  changeTaskContainer(task : TaskCard , containerId : any){
    if(containerId === "cdk-drop-list-0"){
      task.statusId = 1;
      return task;
    }
    else if(containerId === "cdk-drop-list-1"){
      task.statusId = 2;
      return task;
    }
    else{
      task.statusId = 3;
      return task;
    }
  }

  createNewObject() {
    const newTask: TaskCard = new TaskCard();
    this.todo.push(newTask);
    this.sortTasks();
  }

  onSearchTextChanged(searchText: string) {
    this.searchText = searchText;
    this.isSearchEmpty = searchText.trim() === '';
    console.log(this.searchText);
    console.log('Is Search Empty:', this.isSearchEmpty);
  }
}
