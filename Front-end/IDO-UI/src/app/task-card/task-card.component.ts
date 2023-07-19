import { Component, OnInit ,Input, EventEmitter, Output, HostListener } from '@angular/core';
import { TaskCard } from 'src/Models/TaskCard';

@Component({
  selector: 'app-task-card',
  templateUrl: './task-card.component.html',
  styleUrls: ['./task-card.component.css']
})
export class TaskCardComponent implements OnInit {
  isEditing: boolean = false;

  @Output() dataEvent = new EventEmitter<TaskCard>();

  
  @Input() task : TaskCard = new TaskCard();

  constructor() { }

  ngOnInit(): void {
  }

  toggleEditMode() {
    this.isEditing = !this.isEditing;
  }

  enterEditMode() {
    this.isEditing = true;
  }

  exitEditMode() {
    this.isEditing = false;
  }

  sendData() {
    this.task.changeImportance();
      this.dataEvent.emit(this.task);
  }

  saveData(){
    
  }

  saveDataAndToggleEditMode() {
    if (this.isEditing) {
      this.sendData();
      this.saveData();
      this.toggleEditMode();
    }
  }

  @HostListener('click', ['$event'])
  onCardClick(event: Event) {
    event.stopPropagation();
  }

}
