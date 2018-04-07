import fs = require('fs');

export class SvgBuilder {
    body: string[] = [];
    indent: number = 0;

    constructor() {
        this.addHeader()
    }

    save(fileName: string): void {
        this.addFooter()
        let svg = this.body.join('\n')
        console.log(svg, fileName);

        fs.writeFile(fileName, svg, function(err) {
            if(err) {
                return console.log(err);
            }
            console.log("The file was saved!");
        });
    }

    raw(svg: string): void {
        this.insert(svg);
    }

    private addHeader(): void {
        this.insert("header")
        this.indent++
    }

    private addFooter(): void {
        this.indent--
        this.insert("footer")
    }

    // For proper indentation, this should be the only method that calls this.body.push
    private insert(svg: string): void {
        this.body.push('    '.repeat(this.indent) + svg)
    }
}
