import fs = require('fs');

export class SvgBuilder {
    body: string[] = [];

    constructor() {
        this.addHeader()
    }

    save(fileName: string): void {
        this.addFooter()
        let svg = this.body.join('/n')
        console.log(svg, fileName);

        fs.writeFile(fileName, svg, function(err) {
            if(err) {
                return console.log(err);
            }
            console.log("The file was saved!");
        });
    }

    raw(svg: string): void {
        this.body.push(svg);
    }

    private addHeader(): void {
        this.body.push("header")
    }

    private addFooter(): void {
        this.body.push("footer")
    }
}
