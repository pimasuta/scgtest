// method for searching restaurant
var searchRestaurant = new Vue({
    el: '#app',
    data: {
        query: 'Bang Sue',
        info: []
    },
    mounted() {
        this.searchRestaurant();
    },
    methods: {
        searchRestaurant: function (event) {
            return axios
                .get('/api/GoogleMapAPI?keyword=' + this.query)
                .then(response => {
                    var result = response.data.results
                    this.info = result
                })
                .catch(error => {
                    console.log(error)
                })
        }
    }
})