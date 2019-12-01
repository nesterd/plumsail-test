<template>
    <div>
        <b-modal hide-footer  title='Удаление' v-model="modalShow">
            <b-alert v-if="error != null" show variant="danger" dismissible>{{error}}</b-alert>
            <loading :active.sync="isLoading" 
                :can-cancel="false" 
                :is-full-page="false"></loading>
            <h4>Точно удалить {{name}}?</h4>
            <div class='modal-footer'>
                <b-button :disabled="isLoading" @click="close">Отмена</b-button>
                <b-button variant="danger" :disabled="isLoading" @click="deleteSomething">
                    <b-spinner v-if="isLoading" small type="grow"></b-spinner>
                    {{isLoading ? 'Loading...' : 'Удалить'}}
                    </b-button>
            </div>
        </b-modal>
    </div>
    
</template>
<script>
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';

import * as ApiConsts from '../../AppConsts/ApiConsts';

export default {
    components: {Loading},
    data(){
        return {
            isLoading: false,
            error: null,
            id: null,
            name: null,
            modalShow: false
        }
    },
    methods:{
        async deleteSomething(){
            this.isLoading = true;
            const response = await fetch(ApiConsts.SOMETHINGS, {
                method: ApiConsts.DELETE,
                body: JSON.stringify(this.id),
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if(response.ok){
                this.$emit('deleteSomething', this.id)
                this.close();
            }
            else{
                const error = await response.json();
                this.error = error.error; // )))
                this.isLoading = false;
            }
        },
        close(){
            this.modalShow = false;
        },
        open(id, name){
            this.id = id;
            this.name = name;
            this.modalShow = true;
            this.error = null;
        }
    }
}
</script>