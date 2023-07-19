import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TokenService } from '../token.service';
import { Router } from '@angular/router';
import { Subject, debounceTime } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  quoteBannerShow : boolean = false;
  userInfoShow : boolean = false;
  quote : string = "Anything that can go wrong, will go wrong!";

  @Output() createNewObject = new EventEmitter<void>();
  @Output() searchTextChanged = new EventEmitter<string>();
 
  searchText: string = '';
  private searchTextChangedSubject = new Subject<string>();

  onClickCreateNewObject() {
    this.createNewObject.emit();
  }
  constructor(private tokenService : TokenService ,  private router : Router) {

   }

   ngOnInit(): void {
  }


   onSearchTextChanged() {
    this.searchTextChangedSubject.next(this.searchText);
  }

  onClickSearch() {
    this.searchTextChanged.emit(this.searchText);
  }
  logOut(){
    this.tokenService.removeToken();
    this.router.navigate(["/login"]);
  }

  changeQuoteBannerStatus(){
    this.quoteBannerShow = !this.quoteBannerShow;
  }

  changeUserInfoShowStatus(){
    this.userInfoShow = !this.userInfoShow;
  }
}
