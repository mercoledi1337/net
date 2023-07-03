<template>
    <div class="d-flex flex-column align-items-start">
    <h1 v-if="name">Witaj {{ capitalizeFirstLetter(this.name) }}</h1>
    <p class="info">
        <slot name="info"></slot>
    </p>
    <img v-if="this.iconName" :src="require(`@/assets/images/icons/${iconName}.png`)" />
</div>
</template>

<script>
export default {
    
    name: "HelloMessage",
    props: {
        iconName: {
            type: String,
            required: false
        },
        name: {
            type: String,
            required: true
        }
    },  
    
    data(){
        return {
            info: "",
        }
    },
}
</script>


<style lang="scss" scoped>

div {
    margin: 25px 0 50px;
    position: relative;

    h1 {
        font-size: 28px;
    }

    p.info {
        text-align: left;
    }

    img {
        width: 70px;
        position: absolute;
        bottom: 5px;
        right: 0;

        @media (max-width: 768px) { 
            display: none;
        }
    }

}

</style>