import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ToDoListComponent } from './to-do-list/to-do-list.component';
import { AuthGuard } from './auth.guard';
import { TestComponent } from './test/test.component';

const routes: Routes = [
  {path: "", pathMatch: "full",redirectTo: "login"},
  {path : "login" , component : LoginComponent},
  {path : "to-do-list" , component : ToDoListComponent , canActivate: [AuthGuard]},
  {path : "test" , component : TestComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
