class App {
    constructor(rootElement) {
        this.elements = {
            list: rootElement.querySelector('[data-role-list]'),
            search: rootElement.querySelector('[data-role-search]'),
            orderby: rootElement.querySelector('[data-role-orderby'),
            direction: rootElement.querySelector('[data-role-direction]'),
            load: rootElement.querySelector('[data-role-load]'),
            count: rootElement.querySelector('[data-role-count]')
        };
        this.templates = {
            list: this.elements.list.innerHTML
        };

        this.cursor = null;
        
        //Subscribe events
        this.elements.load.addEventListener('click', () => this.load(this.cursor), false);
        this.elements.search.addEventListener('keyup', () => this.load(), false);
        this.elements.orderby.addEventListener('change', () => this.load(), false);
        this.elements.direction.addEventListener('change', () => this.load(), false);
    }

    async request(statement) {
        const response = await fetch('/api', { method: 'POST', body: statement, headers: { 'Content-Type': 'application/graphql' } });
        const json = await response.json();
        return json.data;
    }

    render(template, container, data, append) {
        const compiledTemplate = Handlebars.compile(template);
        const output = compiledTemplate(data);
        container.innerHTML = (append ? container.innerHTML : '') + output;
    }

    async load(cursor, search, orderby, direction) {
        const pageSize = 10;
        orderby = orderby || this.elements.orderby.value || "name";
        direction = direction || this.elements.direction.value || "ASC";
        search = (search || this.elements.search.value || "").replace(/"/g, "\\\"");
        cursor = cursor || "";
        const append = !!cursor;

        const productQuery = `
        query {
            products(first: ${pageSize}, after: "${cursor}", where: { name_contains: "${search}"}, order_by: {${orderby}: ${direction}}) {
              nodes {
                id
                name
                price
                category {
                    name
                }
              }
              totalCount
              pageInfo {
                endCursor
                hasNextPage
              }
            }
        }
        `;
        const data = await this.request(productQuery);
        this.cursor = data.products.pageInfo.endCursor;
        this.render(this.templates.list, this.elements.list, data, append);
        this.elements.load.style.display = data.products.pageInfo.hasNextPage ? 'block' : 'none';
        this.elements.count.innerText = data.products.totalCount;
    }
}