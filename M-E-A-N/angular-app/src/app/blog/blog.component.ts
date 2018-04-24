import { Component, OnInit } from '@angular/core';
import { Item } from '../item'
import { ItemDataService } from '../item-data.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css'],
  providers: [ItemDataService]
})
export class BlogComponent implements OnInit {

  shoppingItemList: Item[]=[];
  selectedItem: Item;
  toggleForm: boolean = false;

  constructor(private itemDataService: ItemDataService) { }

  getItems(){
    this.itemDataService.getShoppingItems()
    .subscribe( items => {
      this.shoppingItemList = items;
      // console.log('data from itemdataservice: ' + this.shoppingItemList[0].itemName);
    });
  }

  addItem(form){
    console.log(form.value);
    let newItem: Item = {
      itemName: form.value.itemName,
      itemQuantity: form.value.itemQuantity,
      itemBought: false
    }
    this.itemDataService.addShoppingItem(newItem)
      .subscribe(item => {
        console.log(item);
        this.getItems();
      });
  }

  deleteItem(id){
    this.itemDataService.deleteShoppingItem(id)
    .subscribe( data => {
      console.log(data);
      if(data.n == 1){
        for(var i=0; i < this.shoppingItemList.length; i++){
          if(id == this.shoppingItemList[i]._id){
            this.shoppingItemList.splice(i, 1);
          }
        }
      }
    });
  }

  editItem(form){
    let newItem: Item = {
      _id: this.selectedItem._id,
      itemName: form.value.itemName,
      itemQuantity: form.value.itemQuantity,
      itemBought: this.selectedItem.itemBought
    }
    this.itemDataService.updateShoppingItem(newItem)
    .subscribe( result => {
      console.log('Original item to be updated with old values:' + result);
      this.getItems();
    });
    this.toggleForm = !this.toggleForm;
  }

  // Data-Binding, Toggle, etc .

  showEditForm(item){
    this.selectedItem = item;
    this.toggleForm = !this.toggleForm;
  }

  updateItemCheckbox(item){
    item.itemBought = !item.itemBought;
    this.itemDataService.updateShoppingItem(item)
    .subscribe( result => {
      console.log('Original checkbox values:' + result.itemBought);
      this.getItems();
    });
  }

  ngOnInit() {
    this.getItems();    
  }

}
