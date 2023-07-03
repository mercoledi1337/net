<template>
    <section id="receptionist-patients-visits">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <breadcrumbs>
                        <router-link to="/wizyty">Wizyty</router-link>
                    </breadcrumbs>
                    <hello-message v-if="user" :name="user.firstName" icon-name="reminder">
                        <template v-slot:info>Oto lista umówionych wizyt przez pacjentów</template>
                    </hello-message>
                    <div class="wrapper d-flex flex-column">
                        <div class="search">
                            <div class="input-group mb-5">
                                <input type="search" class="form-control" v-model='searchQuery' placeholder="Wyszukaj...">
                            </div>
                        </div>
                        <div class="table-responsive d-flex flex-column">  
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th v-for="field in fields" :key='field'> {{ capitalizeFirstLetter(field) }} </th>
                                    </tr>
                                </thead>
                                <tbody v-if="visitsList.length > 0">
                                    <tr v-for="visit in visitsList" :key="visit.id">
                                        <td v-for="field in fields" :key='field'>
                                            <span v-if="field == 'data'">
                                                <span>
                                                    {{ new Date(visit.date).toLocaleDateString('pl', { weekday:"long", year:"numeric", month:"short", day:"numeric"}) }}
                                                </span>
                                            </span>
                                            <span v-else-if="field == 'pacjent'">
                                                <span>
                                                    <img src="@/assets/images/icons/svg/profile.svg">
                                                </span>
                                                <span>
                                                    {{ capitalizeFirstLetter(visit.patient.user.firstName) }} {{  capitalizeFirstLetter(visit.patient.user.lastName) }}   
                                                </span>
                                            </span>
                                            <span v-else-if="field == 'lekarz'">
                                                <span>
                                                    <img src="@/assets/images/icons/svg/profile.svg">
                                                </span>
                                                <span>
                                                    {{ capitalizeFirstLetter(visit.doctor.user.firstName) }} {{  capitalizeFirstLetter(visit.doctor.user.lastName) }}   
                                                </span>
                                            </span>
                                            <span v-else-if="field == 'specjalizacja'">
                                                {{ capitalizeFirstLetter(setSpecialization(visit.doctor.specialization)) }}
                                            </span>
                                            <span v-else-if="field == 'status'">
                                                <span class="status-icon" :style="{backgroundColor: setStatusIcon(visit.status)}"></span>
                                                <span class="status-text">{{ capitalizeFirstLetter(setStatus(visit.status)) }}</span>
                                            </span>
                                            <span v-else>
                                                <span>
                                                    <button @click="confirmVisit(visit.id)" :class="['teal-button', {'disabled-teal-button': visit.status == 'Finished' || visit.status == 'Canceled' || visit.status == 'Confirmed'}]">Potwierdź</button>
                                                    <button @click="cancelVisit(visit.id)" :class="['red-button', {'disabled-red-button': visit.status == 'Finished' || visit.status == 'Canceled'}]">Odwołaj</button>
                                                </span>
                                            </span> 
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody class="no-results" v-else>
                                    <tr>
                                        <td colspan="6">Brak umówionych wizyt</td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>
                                            <span class="tfoot-icon">
                                                <img src="@/assets/images/icons/svg/table_arrow_left.svg">
                                            </span>
                                            <span class="tfoot-text">Poprzednia strona</span></th>
                                        <th v-for="field in fields.length-2" :key="field"></th>  
                                        <th>
                                            <span class="tfoot-text">Następna strona</span>
                                            <span class="tfoot-icon">
                                                <img src="@/assets/images/icons/svg/table_arrow_right.svg">
                                            </span>
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script>
import axios from 'axios'
import jwt_decode from "jwt-decode";
import { mapGetters } from 'vuex';
// import { computed, ref } from "vue";

export default {
    data(){
        return {
            modalIsOpen: false,
            fields: ['data', 'pacjent', 'lekarz', 'specjalizacja', 'status', 'akcje'],
            //filteredList: 
        }
    },  
    computed: {
        ...mapGetters(['user', 'visitsList']),
    },
    
    methods: {
        cancelVisit(id){
            axios.get(`Visits/${id}/Cancel`);
        },
        confirmVisit(id){
            axios.get(`Visits/${id}/Confirm`)
        },
        setSpecialization(specialization){
            switch(specialization){
                case 'oculist':
                    return "okulista"  
                default:
                    return "niezdefiniowana"
            } 
        },
        setStatus(status){
            switch(status){
                case 'Confirmed':
                    return "Potwierdzona"
                case 'Unconfirmed':
                    return "Niepotwierdzona"
                case 'Canceled':
                    return "Odwołana"
                case 'Finished':
                    return "Zakończona"
                default:
                    return "niezdefiniowany"
            }
        },
        setStatusIcon(status) {
            switch(status) {
                case 'Confirmed':
                    return "#209420";
                case 'Unconfirmed':
                    return "#F8EE12"
                case 'Canceled':
                    return "#F84912";
                case 'Finished':
                    return "#205594"
                default:
                    return "#FFF";
            }
        },
        toggleModalIsOpen(){

        }

        // search(props){
        //     let sort = ref(false);
        //     let updatedList =  ref([])
        //     let searchQuery = ref("");
            
        //     const sortedList = computed(() => {
        //         if (sort.value) {
        //             return updatedList.value
        //         } else {
        //             return props.data;
        //         }
        //     });

        //     const filteredList = computed(() => {
        //         return sortedList.value.filter((product) => {
        //             if(product.value){
        //                 return product.value.toLowerCase().indexOf(searchQuery.value.toLowerCase()) != -1;
        //             }
        //             return product;
                    
        //         });
        //     });   
        
        //     return { sortedList, searchQuery, filteredList }
        // }
    },
    async created(){
        const token = localStorage.getItem('token');
        const tokenDecoded = jwt_decode(token);

        const getUserInfo = await axios.get(`Users/${tokenDecoded.nameid}`); // wymagane do działania nawigacji
        const getVisitsInfo = await axios.get(`Visits`);

        await this.$store.dispatch('user', getUserInfo.data.data);
        await this.$store.dispatch('visitsList', getVisitsInfo.data.data);

        console.log(getVisitsInfo);
    },
}
</script>

<style lang="scss" scoped>
div.wrapper {
    margin: 40px 0;
    div.search {
        width: 300px;
        align-self: flex-end;

        @media (max-width: 768px) { 
            align-self: center;
        }

        div.input-group {
            input {
                background-image: url("@/assets/images/icons/svg/magnifying_glass.svg");
                background-repeat: no-repeat;
                background-position-x: 16px;
                background-position-y: 8px;
                background-color: $primary;
                border: 1px solid $secondary;
                padding-left: 48px;
                font-weight: 600;
                color: $secondary;

                &:focus {
                    box-shadow: rgba(0, 0, 0, 0.2) 0px 2px 2px;
                }
            }
        }
    }
    div.table-responsive {
        border: 1px solid $button-light;
        border-radius: 10px;
        background-color: $primary;

        table {
            margin: 0;
            
            thead, tfoot, tbody {
                tr {
                    th, td {
                        text-align: left;
                    }
                }
            }
            thead, tfoot {
                tr {
                    &:hover {
                        background-color: transparent;
                    }
                    th {
                        padding: 24px;
                        font-weight: 600;
                        line-height: 18px;
                        color: $secondary;

                        &:last-child {
                            width: 200px;
                        }
                    }
                }
            }
            thead {
                tr {
                    border-radius: 0 15px 0 15px;
                }
            }
            tbody {
                tr {
                    &:hover {
                        background-color: $button-light-hover;
                    }
                    td {
                        padding: 16px 24px;
                        font-weight: 500;
                        vertical-align: middle;
                        font-size: 14px;
                        white-space: nowrap;
                        

                        span {
                            span {
                                vertical-align: middle;

                                &:first-child {
                                    margin-right: 16px;
                                }
                                &.status-icon {
                                    background-color: setStatus;
                                    width: 15px;
                                    height: 15px;
                                    display: inline-block;
                                    box-shadow: inset 0px 4px 4px rgba(0, 0, 0, 0.25);
                                    border-radius: 100%;
                                    box-sizing: border-box;
                                    margin-right: 10px;
                                    
                                }
                            }
                        }
                        
                        button {
                            border-radius: 5px;
                            padding: 2px 8px;
                            color: white;
                            font-weight: 500;
                            font-size: 13px;
                            line-height: 18px;
                            border: 0;
                            margin-right: 20px;
                            transition: all .2s ease-in-out;

                            &.disabled-teal-button {
                                cursor: not-allowed;
                                background-color: $button-teal-hover !important;
                            }
                            &.disabled-red-button {
                                cursor: not-allowed;
                                background-color: $button-red-hover !important; 
                            }
                            &.blue-button {
                                background-color: $button-blue;

                                &:hover {
                                    background-color: $button-blue-hover;
                                }
                            }

                            &.teal-button {
                                background-color: $button-teal;

                                &:hover {
                                    background-color: $button-teal-hover;
                                }
                            }

                            &.red-button {
                                background-color: $button-red;

                                &:hover {
                                    background-color: $button-red-hover;
                                }
                            }       
                        }
                    }
                }

                &.no-results {
                    tr {
                        td {
                            text-align: center;
                        }
                    }
                }
            }

            tfoot {
                tr {
                    th {
                        border: 0;
                        white-space: nowrap;
                        span.tfoot-text {
                            vertical-align: middle;
                            margin: 0 6px;
                        }
                        &:last-child {
                            text-align: right;
                        }
                    }
                }
            }
        }
    }
}
</style>