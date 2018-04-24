import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import{ HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';


import { AppComponent } from './app.component';
import { TemplateDrivenComponent } from './template-driven/template-driven.component';
import { MyNewComponentComponent } from './my-new-component/my-new-component.component';
import { ListComponent } from './list/list.component';
import { DataListService } from './data-list.service';
import { ShoppingItemComponent } from './shopping-item/shopping-item.component';
import { BlogComponent } from './blog/blog.component';
import { AboutComponent } from './about/about.component';

const ROUTES: Routes = [
  {path: '', component: AboutComponent},
  {path: 'blog', component: ShoppingItemComponent},
  {path: 'about', component: BlogComponent}
]


@NgModule({
  declarations: [
    AppComponent,
    TemplateDrivenComponent,
    MyNewComponentComponent,
    ListComponent,
    ShoppingItemComponent,
    BlogComponent,
    AboutComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    HttpModule,
    ReactiveFormsModule,
    RouterModule.forRoot(ROUTES)
  ],
  providers: [DataListService],
  bootstrap: [AppComponent]
})
export class AppModule { }
