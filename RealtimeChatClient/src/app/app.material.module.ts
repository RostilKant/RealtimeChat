import {NgModule} from '@angular/core';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';

@NgModule(
  {
    exports: [
      MatInputModule,
      MatButtonModule,
      MatFormFieldModule,
      MatCardModule,
      MatIconModule
    ]
  }
)
export class AppMaterialModule{

}
