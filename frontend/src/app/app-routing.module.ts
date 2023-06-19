import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { IndexComponent } from './Pages/index/index.component';
import { FeedComponent } from './Pages/feed/feed.component';

const routes: Routes = [
  { path: "/", component: IndexComponent },
  { path: "/feed", component: FeedComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }