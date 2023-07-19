import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearcheService {

  private searchTextSubject = new BehaviorSubject<string>('');
  searchText$ = this.searchTextSubject.asObservable();

  setSearchText(searchText: string) {
    this.searchTextSubject.next(searchText);
  }


  public currenSearchText : string = "";
  setTextWithoutObservable(text: string){
    this.currenSearchText = text;
  }
  
  constructor() { }
}
