import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TokenService } from '../token.service';
import { Router } from '@angular/router';

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

  onClickCreateNewObject() {
    this.createNewObject.emit();
  }
  constructor(private tokenService : TokenService ,  private router : Router) { }

  ngOnInit(): void {
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
