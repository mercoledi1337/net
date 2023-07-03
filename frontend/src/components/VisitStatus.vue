<template>
    <span class="status-icon" :style="{backgroundColor: setStatusIcon(returnStatus)}"></span>
    <span class="status-text">{{ capitalizeFirstLetter(setStatus(returnStatus)) }}</span>
</template>

<script>
export default {
    name: "VisitStatus",
    props: {
        status: {
            type: String
        }
    },
    computed: {
        returnStatus(){
            return this.status;
        }
    },
    methods: {
        
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
    }
}
</script>   

<style lang="scss" scoped>
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
</style>