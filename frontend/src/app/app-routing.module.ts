import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { AccountComponent } from './Pages/account/account.component';
import { SignupComponent } from './Pages/signup/signup.component';
import { LoginComponent } from './Pages/login/login.component';
import { FeedComponent } from './Pages/feed/feed.component';

const routes: Routes = [
  { path: "", component: FeedComponent },
  { path: "login", component: LoginComponent },
  { path: "signup", component: SignupComponent },
  { path: "account", component: AccountComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }