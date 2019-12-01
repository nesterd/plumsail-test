<template>
    <div>
        <b-modal hide-footer  :title='modalTitle' v-model="modalShow">
            <b-alert v-if="error != null" show variant="danger" dismissible>{{error}}</b-alert>
            <loading :active.sync="isLoading" 
                :can-cancel="false" 
                :is-full-page="false"></loading>
            <b-form class="mb-4">
                <b-form-group label-cols="2" label="Название" >
                    <b-form-input 
                        v-model="somethingForm.name" 
                        placeholder="Введите название"
                        :state="nameValidation">
                    </b-form-input>
                    <b-form-invalid-feedback :state="nameValidation">
                        Название должно быть не короче 3-х символов.
                    </b-form-invalid-feedback>
                </b-form-group>
                <b-form-group label-cols="2" label="Время" >
                    <datetime 
                        v-model="somethingForm.time" 
                        type="datetime" 
                        :input-class="datetimeClass" 
                        placeholder="Введите время">
                    </datetime>
                    <b-form-invalid-feedback :state="timeValidation">
                        Время должно быть указано.
                    </b-form-invalid-feedback>
                </b-form-group>
                <b-form-group label-cols="2" label="Тип" >
                    <b-form-select 
                        v-model="somethingForm.typeId" 
                        :options="typeOptions"
                        :state="typeValidation">
                        <template v-slot:first>
                            <option :value="null" disabled>Выберете тип</option>
                        </template>
                    </b-form-select>
                    <b-form-invalid-feedback :state="typeValidation">
                        Необходимо выбрать тип.
                    </b-form-invalid-feedback>
                </b-form-group>
                <b-form-group label-cols="2" label="Статус" :state="statusValidation">
                    <b-form-radio-group class="mt-2"
                        v-model="somethingForm.status"
                        :options="statusOptions"
                    ></b-form-radio-group>
                    <b-form-invalid-feedback :state="statusValidation">
                        Необходимо выбрать статус.
                    </b-form-invalid-feedback>
                </b-form-group>
                <b-form-checkbox 
                        class="offset-md-2 mb-2"
                        v-model="somethingForm.isImportant">
                    Очень важно
                </b-form-checkbox>
                <b-form-group  label-cols="2" label="Коммент" v-if="somethingForm.isImportant" >
                    <b-form-input v-model="somethingForm.comment" placeholder="Введите комментарий"></b-form-input>
                </b-form-group>
            </b-form>
            <div class='modal-footer'>
                <b-button :disabled="isLoading" @click="close">Отмена</b-button>
                <b-button variant="success" :disabled="isLoading || !totalValidation" @click="save">
                    <b-spinner v-if="isLoading" small type="grow"></b-spinner>
                    {{isLoading ? 'Loading...' : 'Ok'}}
                    </b-button>
            </div>
        </b-modal>
    </div>
    
</template>
<script>
import moment from 'moment';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';

import { Datetime } from 'vue-datetime';
import 'vue-datetime/dist/vue-datetime.css';

import * as ApiConsts from '../../AppConsts/ApiConsts';
import {DATETIME_FORMAT} from '../../AppConsts/Consts';

export default {
    components: {Loading, Datetime},
    data(){
        return {
            isLoading: false,
            error: null,
            somethingForm: {},
            modalShow: false,
            typeOptions: [],
            statusOptions: [],
            isNewForm: false
        }
    },
    computed:{
        modalTitle(){
            return `${this.isNewForm ? 'Добавление' : 'Редактирование'}`;
        },
        totalValidation(){
            return this.nameValidation && this.typeValidation && this.timeValidation && this.statusValidation;
        },
        nameValidation(){
            return this.somethingForm.name != null && this.somethingForm.name.length  >= 3;
        },
        typeValidation(){
            return this.somethingForm.typeId != null;
        },
        timeValidation(){
            return this.somethingForm.time != null && this.somethingForm.time.length > 1;
        },
        statusValidation(){
            return this.somethingForm.status != null;
        },
        datetimeClass(){
            let cssClass = `form-control ${this.timeValidation ? "is-valid" : "is-invalid"}`;
            return cssClass;
        }


    },
    methods:{
        async save(){
            this.isLoading = true;
            const form = this.somethingForm;
            const something = {
                id: form.id,
                name: form.name,
                typeId: form.typeId,
                time: moment(form.time).format(DATETIME_FORMAT),
                status: form.status,
                isImportant: form.isImportant,
                comment: form.comment
            };

            const method = this.isNewForm ? ApiConsts.POST : ApiConsts.PUT;

            const response = await fetch(ApiConsts.SOMETHINGS, {
                method: method,
                body: JSON.stringify(something),
                headers: {
                    'Content-Type': 'application/json'
                }
            })

            const data = await response.json();
            if(response.ok){
                
                if(this.isNewForm) this.$emit('addSomething', data); 
                else this.$emit('editSomething', data);

                this.modalShow = false;
            }
            else{
                this.error = data.error;
                this.isLoading = false;
            }
        },
        close(){
            this.modalShow = false;
        },
        clear(){
            this.typeOptions = [];
            this.statusOptions = [];
            this.error = null;
        },
        async open(something){
            this.isNewForm = something == null;

            this.clear();
            this.isLoading = true;
            this.somethingForm = this.getForm(something);
            this.modalShow = true;
            await this.populateOptions();
        },
        async populateOptions(){
            const response = await fetch(ApiConsts.SOMETHINGS_OPTIONS);
            const options = await response.json();

            this.typeOptions = options.typeOptions;
            this.statusOptions = options.statusOptions;

            this.isLoading = false;
        },
        getForm(something){
            if(something == null) return {id: 0, typeId: null};
            return {
                id: something.id,
                name: something.name,
                typeId: something.typeId,
                time: moment(something.time, DATETIME_FORMAT).format(),// "2019-12-21T08:24", //new Date(something.time), 
                status: something.status,
                isImportant: something.isImportant,
                comment: something.comment
            };
        }

    }
}
</script>