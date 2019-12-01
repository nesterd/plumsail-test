<template>
  <div>
    <edit-something-modal @addSomething='addSomething' @editSomething='editSomething' ref='edit-modal'></edit-something-modal>
    <delete-something-modal ref='delete-modal' @deleteSomething='deleteSomething'></delete-something-modal>
    <loading :active.sync="isLoading" 
        :can-cancel="false" 
        :is-full-page="true"></loading>
    <h2>Список чего-то</h2>
    <b-row class="filter">
      <b-col>
        <b-form-input
              size="sm"
              v-model="filter"
              type="search"
              id="filterInput"
              placeholder="Название"
            ></b-form-input>
      </b-col>
      <b-col>
        <b-button size="sm" variant="success" class="float-right" @click="openModal()">Добавить</b-button>
      </b-col>
    </b-row>
    <b-table small hover :items="something_list" :fields="fields" :filter="filter" sort-icon-left>
      <template v-slot:cell(actions)="row" class="float-right">
        <div class="float-right">
          <b-button variant="link" size="sm" @click="openModal(row.item)" >
            редактировать
          </b-button>
          |
          <b-button variant="link" size="sm"  @click="$refs['delete-modal'].open(row.item.id, row.item.name)">
            удалить
          </b-button>
        </div>
      </template>
    </b-table>
  </div>
</template>

<script>
// import { Datetime } from 'vue-datetime';
// import 'vue-datetime/dist/vue-datetime.css'
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import EditSomethingModal from '../components/Somethings/EditSomethingModal';
import DeleteSomethingModal from '../components/Somethings/DeleteSomethingModal';
import {SOMETHINGS} from '../AppConsts/ApiConsts';

export default {
  name: 'somethings',
  components:{Loading, EditSomethingModal, DeleteSomethingModal},
  data(){
    return {
      isLoading: true,
      filter: null,
      fields:[
        {
          key: 'name',
          sortable: true,
          label: 'Название'
        },
        {
          key: 'typeName',
          sortable: true,
          label: 'Тип'
        },
        {
          key: 'time',
          sortable: true,
          label: 'Время'
        },
        {
          key: 'statusName',
          sortable: true,
          label: 'Статус'
        },
        {
          key: 'isImportant',
          sortable: true,
          sortByFormatted: true,
          filterByFormatted: true,
          label: "Особо важный",
          formatter: (value) => {
              return value ? 'Да' : 'Нет'
            },
        },
        {
          key: 'actions',
          label: ''
        }
      ],
      something_list: []
    }
  },
  mounted(){
    this.populateData();
  },
  methods:{
    openModal(item){
      this.$refs['edit-modal'].open(item);
    },
    async populateData(){
      const response = await fetch(SOMETHINGS);
      if(response.ok){
        const data = await response.json();
        this.something_list = data;
      }
      this.isLoading = false;
    },
    addSomething(something){
      this.something_list.push(something);
    },
    editSomething(something){
      let index = this.something_list.findIndex(x => x.id == something.id);
      this.$set(this.something_list, index, something);
    },
    deleteSomething(id){
      let index = this.something_list.findIndex(x => x.id == id);
      this.something_list.splice(index, 1);
    }
  }
}
</script>

<style>
  .filter{
    margin-bottom: 10px;
  }
</style>
