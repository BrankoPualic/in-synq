import { CommonModule } from "@angular/common";
import { Component, input, OnInit } from "@angular/core";
import { PageLoaderService } from "../services/page-loader.service";

@Component({
    selector: 'app-loader',
    standalone: true,
    imports: [CommonModule],
    template: `@if(isVisible){<div class="backdrop"><span class="loader"></span></div>}
    @if(containerLoader()){<div class="container-backdrop"><span class="loader"></span></div>}`,
    styles: `
    .loader {
        width: 48px;
        height: 48px;
        border-radius: 50%;
        display: inline-block;
        border-top: 3px solid #FFF;
        border-right: 3px solid transparent;
        box-sizing: border-box;
        animation: rotation 1s linear infinite;
    }
    @keyframes rotation {
    0% {
        transform: rotate(0deg);
    }
    100% {
        transform: rotate(360deg);
    }
    }`
})
export class LoaderComponent implements OnInit {
    containerLoader = input<boolean>(false);
    isVisible = false;

    constructor(private loaderService: PageLoaderService) { }

    ngOnInit(): void {
        this.loaderService.loaderState$.subscribe(_ => this.isVisible = _);
    }
}