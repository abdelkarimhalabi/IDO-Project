import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TokenService } from '../token.service';
import { Router } from '@angular/router';
import { Subject, debounceTime } from 'rxjs';
import { SearcheService } from '../searche.service';

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
  // @Output() searchTextChanged = new EventEmitter<string>();
  @Output() searchButtonClicked = new EventEmitter<void>();
 
  public searchText: string = '';
  private searchTextChangedSubject = new Subject<string>();

  onClickCreateNewObject() {
    this.createNewObject.emit();
  }
  constructor(private tokenService : TokenService ,  private router : Router , private searchService : SearcheService ) {

   }

   ngOnInit(): void {
  }


  onClickSearch() {
    this.searchButtonClicked.emit();
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
  onSearchInputChanged(e:any) {
    this.searchService.setSearchText(e.target.value);
  }

  search(e : any){
    //this.searchService.setTextWithoutObservable(e.target.value);
  }

}
