import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { NewForumsComponent } from './pages/new-forums/new-forums.component';
import { MyForumsComponent } from './pages/my-forums/my-forums.component';
import { AccountComponent } from './pages/account/account.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { ForumComponent } from './pages/forum/forum.component';
import { FeedComponent } from './pages/feed/feed.component';
import { Page404Component } from './pages/page404/page404.component';

const routes: Routes = [
  { path: '', component: FeedComponent },
  { path: 'signin', component: SignInComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'account', component: AccountComponent },
  { path: 'myforums', component: MyForumsComponent },
  { path: 'newforums', component: NewForumsComponent },
  { path: 'forum/:title', component: ForumComponent },
  { path: '**', component: Page404Component }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }