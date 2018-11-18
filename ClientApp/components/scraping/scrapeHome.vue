<template>
<v-container>
    <scrape-form></scrape-form>
    <scrape-results></scrape-results>

</v-container>
    
</template>
<script>
import ScrapeForm from './scrapeForm'
import ScrapeResults from './scrapeResults'

export default {
    name: 'ScrapeHome',
    components:{
        ScrapeForm,
        ScrapeResults
    }
    
}
</script>
