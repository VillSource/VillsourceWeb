import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, ElementRef, OnInit } from '@angular/core';

@Component({
  selector: 'app-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <form (submit)="onType(el)">
      <input type="text" #el />
    </form>
    <button (click)="delete('**')">delete all</button>

    @for (item of list; track item) {
    <li [id]="item.id">{{ item.content }} >> <button (click)="delete(item.id)">delete</button></li>
    } @empty { There are no items. }
  `,
  styleUrl: './list.component.css',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class ListComponent implements OnInit {
  list: { id: number; content: string }[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.listFunc();
  }

  public onType(el: any): boolean {
    let txt = el.value;
    el.value = '';

    this.add(txt)

    return false;
  }

  add(txt: string) {
    this.http.post(`/api/list?txt=${txt}`, null).subscribe(()=>this.listFunc(), e=>alert("slow down bro!"));
  }

  listFunc() {
    this.http.get('/api/list').subscribe((res) => {
      this.list = res as { id: number; content: string }[];
    });
  }

  delete(idl:string|number){
    console.log("delete", idl)
    this.http.delete(`/api/list?id=${idl}`)
    .subscribe(()=>this.listFunc());
  }
}
