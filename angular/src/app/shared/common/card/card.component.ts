import { Component, Input, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'card',
  templateUrl: './card.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./card.component.less']
})
export class CardComponent {
  /**
   * Title of the card
   */
  @Input() title: string;
  @Input() message: string;
  @Input() limit: number = 150;

  hide: boolean = true;

  toggle(): void {
        this.hide = !this.hide;
    }
}
