<template>
    <div>
        <span> {{ isPatient ? "Panel pacjenta" : "Panel pracownika" }} </span>
        <slot></slot>
    </div>
</template>

<script>
    export default {
        name: "Breadcrumbs",
        props: {
            isPatient: {
                type: Boolean,
                required: true
            }
        }
    }

</script>

<style lang="scss" scoped>
    div {
        font-weight: 600;
        color: $secondary;
        text-align: left;
        margin-top: 30px;

        :slotted(a) {
            text-decoration: none;
            color: $secondary;

            &:hover {
                text-decoration: underline;
            }

            &::before {
                content: url("@/assets/images/icons/svg/breadcrumbs_arrow.svg");
                margin: 0 15px;
            }
        }
    }
</style>