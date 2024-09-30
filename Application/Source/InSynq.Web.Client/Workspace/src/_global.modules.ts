import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { EnumNamePipe } from "./app/pipes/enum-name.pipe";

export const GLOBAL_MODULES = [CommonModule, RouterModule, FormsModule, FontAwesomeModule, EnumNamePipe];