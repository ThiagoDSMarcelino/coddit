import { NavigationStart, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';

import { UserService } from 'src/app/Services/UserService/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: true,
  imports: [NgIf],
})
export class NavbarComponent implements OnInit {
  constructor(
    private service: UserService,
    private router: Router) {
    router.events.subscribe((routeEvent) => {
      if (routeEvent instanceof NavigationStart) {
        this.ValidateLogin();
      }
    });
  }
  IsLogged: boolean = false;

  ngOnInit = () => {
    this.ValidateLogin()
  }

  ValidateLogin = () => {
    var token = sessionStorage.getItem("token")

    if (token !== null) {
      this.IsLogged = true
    }
  }

  LogOut = () => {
    sessionStorage.removeItem("token")
    window.location.reload();
  }
}