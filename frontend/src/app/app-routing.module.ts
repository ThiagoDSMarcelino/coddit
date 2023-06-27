import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { AccountComponent } from './Pages/account/account.component';
import { SignUpComponent } from './Pages/sign-up/sign-up.component';
import { SignInComponent } from './Pages/sign-in/sign-in.component';
import { FeedComponent } from './Pages/feed/feed.component';

const routes: Routes = [
  { path: "", component: FeedComponent },
  { path: "sign in", component: SignInComponent },
  { path: "sign up", component: SignUpComponent },
  { path: "account", component: AccountComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }