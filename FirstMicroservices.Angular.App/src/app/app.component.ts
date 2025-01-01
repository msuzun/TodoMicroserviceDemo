import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  todos: TodoModel[] = [];
  work: string = "";
  name: string = "";
  categories: CategoryModel[] = [];
  constructor(private http:HttpClient){
    this.getAllTodos()
    this.getAllCategory();
  }
  getAllTodos(){
    this.http.get<TodoModel[]>("http://localhost:5000/api/todos/getall").subscribe(res=>{
      this.todos = res
    },
    error => {
      console.error('API hatası:', error); // Hata durumunda loglama
    })
  }
  saveTodo(){
    this.http.get("http://localhost:5000/api/todos/create?work=" + this.work).subscribe(()=>{
      this.getAllTodos();
    },
    error => {
      console.error('API hatası:', error); // Hata durumunda loglama
    })
  }
  getAllCategory(){
    this.http.get<CategoryModel[]>("http://localhost:5000/api/categories/getall").subscribe(res=>{
      this.categories = res
    },
    error => {
      console.error('API hatası:', error); // Hata durumunda loglama
    })
  }
  saveCategory(){
    const data={
      name:this.name
    }
    this.http.post("http://localhost:5000/api/todos/create",data).subscribe(()=>{
      this.getAllCategory();
    },
    error => {
      console.error('API hatası:', error); // Hata durumunda loglama
    })
  }
}
export class TodoModel{
  id: number = 0;
  work: string = "";
}
export class CategoryModel{
  id: number = 0;
  name: string = "";
}