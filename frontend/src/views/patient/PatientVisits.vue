<template>
    <section id="patient-visits">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <breadcrumbs is-patient>
                        <router-link to="/moje-wizyty">Moje wizyty</router-link>
                    </breadcrumbs>
                    <hello-message v-if="user" :name="user.firstName" icon-name="reminder"><template v-slot:info>Oto lista Twoich wizyt</template></hello-message>
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
                                        <th v-for="field in fields" :key='field'> {{ field }} </th>
                                    </tr>
                                </thead>
                                <tbody v-if="patientVisitsList.length > 0">
                                    <tr v-for="visit in patientVisitsList" :key="visit.id">
                                        <td v-for="field in fields" :key='field'>
                                            <span v-if="field == 'data'">
                                                <span>
                                                    {{ new Date(visit.date).toLocaleDateString('pl', { weekday:"long", year:"numeric", month:"short", day:"numeric"}) }}
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
                                                {{ capitalizeFirstLetter(visit.doctor.specialization) }}
                                            </span>
                                            <span v-else-if="field == 'status'">
                                                <visit-status :status="visit.status"></visit-status>
                                            </span>
                                            <span v-else>
                                                <span>
                                                    <button @click="cancelVisit(visit.id)" :class="['red-button', {'disabled-red-button': visit.status == 'Finished' || visit.status == 'Canceled'}]">Odwołaj</button>
                                                </span>
                                            </span>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody class="no-results" v-else>
                                    <tr>
                                        <td colspan='5'>Brak umówionych wizyt</td>
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
import VisitStatus from "@/components/VisitStatus.vue";

export default {
    data(){
        return {
            fields: ['data', 'lekarz', 'specjalizacja', 'status', 'akcje'],
            rerenderStatus: 0,
        }
    },
    components: {
        VisitStatus
    },
    computed: {
        ...mapGetters(['user', 'patientVisitsList']),
    },
    async created(){
        const token = localStorage.getItem('token');
        const tokenDecoded = jwt_decode(token);
        const getPatientVisits = await axios.get(`https://localhost:7121/api/Uzytkownik/Patients/${tokenDecoded.roleId}/Visits`);
        await this.$store.dispatch('patientVisitsList', getPatientVisits.data.data);
        console.log(getPatientVisits);
    },
    methods: {
        forceRerenderStatus(){
            this.rerenderStatus += 1;
        },
        setSpecialization(specialization){
            switch(specialization){
                case 'oculist':
                    return "Okulista"
                default:
                    return "niezdefiniowana"
            }
        },
        cancelVisit(id){
            axios.get(`Visits/${id}/Cancel`);
        }
    }
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
        margin: 40px 0;

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