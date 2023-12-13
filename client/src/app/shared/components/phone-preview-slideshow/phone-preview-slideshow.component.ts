import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-phone-preview-slideshow',
  standalone: true,
  imports: [],
  templateUrl: './phone-preview-slideshow.component.html',
  styleUrl: './phone-preview-slideshow.component.scss'
})
export class PhonePreviewSlideshowComponent implements OnInit, OnDestroy {
  @ViewChild('phoneImg') img!: ElementRef;
  imgIndex: number = 2;
  interval: any;
  timeout: any;
  
  ngOnInit(): void {
    this.startSlideShow();
  }

  ngOnDestroy(): void {
    clearInterval(this.interval);
    clearTimeout(this.timeout)
  }

  startSlideShow(): void {
    this.interval = setInterval(() => {
      this.imgIndex !== 3 ? this.imgIndex++ : (this.imgIndex = 1);
      const imgElement = document.querySelector('.phone') as HTMLImageElement;
      imgElement.style.opacity = '0';
      this.timeout = setTimeout(() => {
          imgElement.src = `/assets/images/phone-${this.imgIndex}.png`;
          imgElement.style.opacity = '1';
      }, 400);
    }, 5000);
  }
}
